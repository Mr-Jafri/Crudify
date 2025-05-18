import { Form, RHFInput } from "@/components/hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z as zod } from "zod";
import { useForm } from "react-hook-form";
import { useEffect, useState } from "react";
import axios from "@/services/axios";
import { redirect } from "next/navigation";

export const NewStudentSchema = zod.object({
  fullName: zod
    .string()
    .min(1, { message: "Full name is required!" })
    .min(3, { message: "Full name must be at least 3 characters long!" }),

  email: zod
    .string()
    .min(1, { message: "Email is required!" })
    .email({ message: "Email must be a valid email address!" }),

  phone: zod
    .string()
    .min(10, { message: "Phone number must be at least 10 digits!" })
    .regex(/^[0-9]+$/, { message: "Phone number must contain only digits!" }),

  dob: zod.string().min(1, { message: "Date of birth is required!" }),
});

// ----------------------------------------------------------------------

export function StudentNewEditForm({ userId }: { userId?: string }) {
  const defaultValues = {
    fullName: "",
    email: "",
    phone: "",
    dob: "",
  };

  const methods = useForm({
    resolver: zodResolver(NewStudentSchema),
    defaultValues,
    mode: "onSubmit",
  });

  const {
    handleSubmit,
    reset,
    formState: { isSubmitting },
  } = methods;

  const onSubmit = handleSubmit(async (data) => {
    console.log(data, "data");
    const res = await axios.post("/api/students", data);
    console.log(res, "res");
    redirect('/students');
  });

  const [student, setStudent] = useState({ id: 0, fullName: '', email: '', dob: '', phone: '' });

  const handleGetCurrentUser = async () => {
    // fetch the current user data with id using axios
    const res = await axios.get(`/api/students/${userId}`);
    console.log(res, "res");
    setStudent(res.data);
  };

  

  useEffect(() => {
    debugger;
    if (userId) {
      handleGetCurrentUser();
    }
  }, []);

  return (
    <div className="p-8">
      <h1 className="text-2xl font-bold mb-4">Add Student</h1>
      <Form methods={methods} onSubmit={onSubmit}>
        <RHFInput name="fullName" type="text" value={student.fullName} helperText="Enter full name" />
        <RHFInput name="email" type="email" value={student.email} helperText="Enter email address" />
        <RHFInput name="phone" type="text" value = {student.phone}helperText="Enter phone number" />
        <RHFInput name="dob" value={student.dob} type="date" helperText="Select date of birth" />
        <button
          type="submit"
          className="mt-4 px-4 py-2 bg-blue-600 text-white rounded"
        >
          {isSubmitting ? "Loading..." : "Submit"}
        </button>
      </Form>
    </div>
  );
}
