"use client";

import ProtectedRoute from "@/auth/protectedRoute";
import StudentListForm from "../student-list-form";

// ----------------------------------------------------------------------

export function StudentListView() {
  return (
    <ProtectedRoute>
      <StudentListForm />
    </ProtectedRoute>
  );
}