"use client";

import { StudentNewEditForm } from "../student-new-edit-form";

// ----------------------------------------------------------------------

export function UserEditView({ userId }: { userId: string }) {
  return <StudentNewEditForm userId={userId} />;
}
