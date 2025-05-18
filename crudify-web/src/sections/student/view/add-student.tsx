"use client";

import ProtectedRoute from "@/auth/protectedRoute";
import { StudentNewEditForm } from "../student-new-edit-form";

// ----------------------------------------------------------------------

export function StudentCreateView() {
  return (
    <ProtectedRoute>
      <StudentNewEditForm />
    </ProtectedRoute>
  );
}
