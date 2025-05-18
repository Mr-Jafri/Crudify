"use client";

import { createContext } from "react";

export interface AuthUser {
  id: string;
  email: string;
  role?: string;
  accessToken?: string;
  [key: string]: any;
}

export interface AuthContextType {
  user: AuthUser | null;
  checkUserSession: () => Promise<void>;
  loading: boolean;
  authenticated: boolean;
  unauthenticated: boolean;
}

// ----------------------------------------------------------------------

export const AuthContext = createContext(undefined);
