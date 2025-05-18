"use client";

import { useSetState } from "minimal-shared/hooks";
import { useMemo, useEffect, useCallback, ReactNode } from "react";
import { AuthContext, AuthContextType, AuthUser } from "../auth-context";
import axios, { endpoints } from "@/services/axios";

import { JWT_STORAGE_KEY } from "./constant";
import { setSession, isValidToken } from "./utils";

interface AuthProviderProps {
  children: ReactNode;
}

interface AuthState {
  user: AuthUser | null;
  loading: boolean;
}

export function AuthProvider({ children }: AuthProviderProps) {
  const { state, setState } = useSetState<AuthState>({
    user: null,
    loading: true,
  });

  const checkUserSession = useCallback(async () => {
    try {
      const accessToken = sessionStorage.getItem(JWT_STORAGE_KEY);

      if (accessToken && isValidToken(accessToken)) {
        setSession(accessToken);

        const res = await axios.get(endpoints.auth.loggedUser);

        if (res?.status === 401) {
          sessionStorage.removeItem(JWT_STORAGE_KEY);
          setState({ user: null, loading: false });
          return;
        }

        const user: AuthUser = res?.data;

        setState({ user: { ...user, accessToken }, loading: false });
      } else {
        setState({ user: null, loading: false });
      }
    } catch (error) {
      console.error(error);
      setState({ user: null, loading: false });
    }
  }, [setState]);

  useEffect(() => {
    checkUserSession();
  }, [checkUserSession]);

  const checkAuthenticated = state.user ? "authenticated" : "unauthenticated";

  const status = state.loading ? "loading" : checkAuthenticated;

  const memoizedValue: AuthContextType = useMemo(
    () => ({
      user: state.user
        ? { ...state.user, role: state.user?.role ?? "admin" }
        : null,
      checkUserSession,
      loading: status === "loading",
      authenticated: status === "authenticated",
      unauthenticated: status === "unauthenticated",
    }),
    [checkUserSession, state.user, status]
  );

  return (
    <AuthContext.Provider value={memoizedValue}>
      {children}
    </AuthContext.Provider>
  );
}
