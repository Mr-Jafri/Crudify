import { Inter } from "next/font/google";
import type { Metadata } from "next";

import { AuthProvider } from "@/auth/context/jwt";

import "./globals.css";

const inter = Inter({ subsets: ["latin"] });

export const metadata: Metadata = {
  title: "Student Management",
  description:
    "Manage students using Next.js, TypeScript, Axios, and TailwindCSS",
};

export default function RootLayout({
  children,
}: {
  readonly children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <body className={inter.className}>
        <AuthProvider>{children}</AuthProvider>
      </body>
    </html>
  );
}
