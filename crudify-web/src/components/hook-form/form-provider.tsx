import { ReactNode } from "react";
import { FormProvider as RHFForm, UseFormReturn } from "react-hook-form";

// ----------------------------------------------------------------------

type FormProps = {
  children: ReactNode;
  onSubmit: React.FormEventHandler<HTMLFormElement>;
  methods: UseFormReturn<any>;
  className?: string;
};

export function Form({
  children,
  onSubmit,
  methods,
  className,
}: Readonly<FormProps>) {
  return (
    <RHFForm {...methods}>
      <form
        onSubmit={onSubmit}
        className={className}
        noValidate
        autoComplete="off"
      >
        {children}
      </form>
    </RHFForm>
  );
}
