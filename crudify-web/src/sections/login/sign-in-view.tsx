// app/login/page.tsx
"use client";

import React from "react";
import { Form, RHFInput } from "@/components/hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z as zod } from "zod";
import { useForm } from "react-hook-form";
import SubmitButton from "@/components/button";
import { jwtDecode, signInWithPassword } from "@/auth/context/jwt";
import { useAuthContext } from "@/hooks/use-auth-context";
import { useRouter } from "next/navigation";

// ✅ Updated Schema Validation
export const LoginSchema = zod.object({
  email: zod
    .string()
    .min(1, { message: "Email is required!" })
    .email({ message: "Email must be a valid email address!" }),
  password: zod.string().min(1, { message: "Password is required!" }),
});

export default function LoginPage() {
  const router = useRouter();

  const { checkUserSession } = useAuthContext();

  const defaultValues = {
    email: "",
    password: "",
  };

  const methods = useForm({
    resolver: zodResolver(LoginSchema),
    defaultValues,
    mode: "onSubmit", // Only validate on submit
  });

  const {
    handleSubmit,
    formState: { isSubmitting },
  } = methods;

  const onSubmit = handleSubmit(async (data) => {
    console.log(data, "data");

    const res = await signInWithPassword({
      email: data.email,
      password: data.password,
    });

    console.log(res, "res");

    // Adjust this according to the actual structure returned by signInWithPassword

    if (res.status === 200 && res?.token) {
      const userData = jwtDecode(res?.token);

      console.log(userData, "userData");

      router.push("/students");
      await checkUserSession?.();
    }
  });

  return (
    <div className="flex items-center justify-center min-h-screen bg-gray-100">
      <div className="w-full max-w-md p-8 space-y-6 bg-white rounded-lg shadow-md">
        <h2 className="text-2xl font-bold text-center text-gray-700">
          Login to your account
        </h2>

        <Form className="space-y-4" onSubmit={onSubmit} methods={methods}>
          <RHFInput name="email" />
          <RHFInput name="password" />

          <SubmitButton isSubmitting={isSubmitting} text="Login" />
        </Form>
      </div>
    </div>
  );
}
