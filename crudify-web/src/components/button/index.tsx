"use client";

import React from "react";

type SubmitButtonProps = {
  readonly isSubmitting: boolean;
  readonly text?: string;
};

export default function SubmitButton({
  isSubmitting,
  text = "Submit",
}: SubmitButtonProps) {
  return (
    <button
      type="submit"
      disabled={isSubmitting}
      className={`mt-4 px-4 py-2 w-full flex items-center justify-center rounded text-white transition-colors
        ${
          isSubmitting
            ? "bg-blue-400 cursor-not-allowed"
            : "bg-blue-600 hover:bg-blue-700"
        }`}
    >
      {isSubmitting ? <>Loading...</> : text}
    </button>
  );
}
