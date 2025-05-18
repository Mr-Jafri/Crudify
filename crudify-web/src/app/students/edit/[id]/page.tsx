'use client';
import { UserEditView } from "@/sections/student/view";
import { useParams } from "next/navigation";
import { useEffect, useState } from "react";

// ----------------------------------------------------------------------

export default function Page() {
    const { id } = useParams();

  if (!id) return <p>Loading...</p>;

  return <UserEditView userId={id} />;
}
