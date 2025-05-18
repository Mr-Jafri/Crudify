import { Controller, useFormContext } from "react-hook-form";
import type { InputHTMLAttributes, ReactNode } from "react";

// ----------------------------------------------------------------------

interface RHFInputProps
  extends Omit<InputHTMLAttributes<HTMLInputElement>, "name"> {
  name: string;
  helperText?: ReactNode;
  label?: string; // Optional label prop
}

export function RHFInput({
  name,
  helperText,
  label,
  type = "text",
  ...other
}: RHFInputProps) {
  const { control } = useFormContext();
  const isNumberType = type === "number";
  const inputId = `input-${name}`;

  return (
    <Controller
      name={name}
      control={control}
      render={({ field, fieldState: { error } }) => (
        <div style={{ display: "flex", flexDirection: "column", marginBottom: 16 }}>
          {label && (
            <label htmlFor={inputId} style={{ marginBottom: 4, fontWeight: 500 }}>
              {label}
            </label>
          )}

          <input
            {...field}
            id={inputId}
            value={field.value ?? ""}
            type={isNumberType ? "text" : type}
            inputMode={isNumberType ? "decimal" : undefined}
            pattern={isNumberType ? "[0-9]*\\.?[0-9]*" : undefined}
            autoComplete="off"
            {...other}
            style={{
              border: error ? "1px solid red" : "1px solid #ccc",
              padding: "8px",
              borderRadius: "4px",
              outline: "none",
            }}
          />

          <small style={{ color: error ? "red" : "#666", marginTop: 4 }}>
            {error?.message ?? helperText}
          </small>
        </div>
      )}
    />
  );
}
