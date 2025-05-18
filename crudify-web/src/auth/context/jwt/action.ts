"use client";

import axios, { endpoints } from "@/services/axios";
import { setSession } from "./utils";

interface SignInParams {
  email: string;
  password: string;
}

interface SignInResponse {
  token?: string;
  success?: boolean;
}

/** **************************************
 * Sign in
 *************************************** */

// ----------------------------------------------------------------------
export const signInWithPassword = async ({
  email,
  password,
}: SignInParams): Promise<{
  status: number;
  success: boolean;
  token: string;
}> => {
  try {
    const params = { email, password };

    const res = await axios.post<SignInResponse>(endpoints.auth.signIn, params);
    console.log(res, "res");
    const { token, success } = res.data;

    if (!token) {
      throw new Error("Check your input and try again");
    }

    if (success) {
      setSession(token);
      axios.defaults.headers.common.Authorization = `Bearer ${token}`;
      return { status: 200, success, token };
    }

    throw new Error("Unknown error during login");
  } catch (error) {
    console.error("Error during sign in:", error);
    throw error;
  }
};

/** **************************************
 * Sign out
 *************************************** */

// ----------------------------------------------------------------------

export const signOut = async () => {
  try {
    await setSession(null);
  } catch (error) {
    console.error("Error during sign out:", error);
    throw error;
  }
};
