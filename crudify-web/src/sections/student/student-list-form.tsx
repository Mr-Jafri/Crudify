"use client";

import { useEffect, useState } from "react";
import { getStudents, deleteStudent } from "@/services/studentService";
import Link from "next/link";
import { signOut } from "@/auth/context/jwt";
import { redirect } from "next/navigation";
import { useAuth } from "@/auth/useAuth";

interface Student {
    id: number;
    fullName: string;
    email: string;
    dateofBirth: Date;
}

export default function StudentListForm() {
    const [students, setStudents] = useState<Student[]>([]);
    const { user } = useAuth();

    useEffect(() => {
        fetchStudents();
    }, []);

    const fetchStudents = async () => {
        // debugger;
        const res: any = await getStudents();
        // debugger;
        console.log(res?.data, "data");
        setStudents(res?.data?.payload);
    };

    const handleDelete = async (id: number) => {
        await deleteStudent(id);
        fetchStudents();
    };

    const onLogout = (() => {
        signOut();
        redirect('/login');
    });


    return (
        <div className="p-8">
            <div className="flex justify-between mb-4">
                <h1 className="text-2xl font-bold">Student List</h1>
                <Link href="/students/add">
                    <button className="bg-green-500 text-white px-4 py-2 rounded">
                        Add Student
                    </button>
                </Link>
                <button
                    type="button"
                    className="bg-green-500 text-white px-4 py-2 rounded" onClick={onLogout}>
                    Logout
                </button>
            </div>
            <table className="w-full table-auto border">
                <thead>
                    <tr className="bg-gray-100">
                        <th className="border px-4 py-2">Name</th>
                        <th className="border px-4 py-2">Email</th>
                        <th className="border px-4 py-2">DOB</th>
                        <th className="border px-4 py-2">Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {students?.length > 0 ? (
                        students.map((student) => (
                            <tr key={student.id}>
                                <td className="border px-4 py-2">
                                    {student.fullName ?? "N/A"}
                                </td>
                                <td className="border px-4 py-2">{student.email ?? "N/A"}</td>
                                <td className="border px-4 py-2">
                                    {student.dob ?? "N/A"}
                                </td>
                                {user.role === "admin" && (
                                    <td className="border px-4 py-2 space-x-2">
                                        <Link href={`/students/edit/${student.id}`}>
                                            <button className="bg-yellow-500 text-white px-3 py-1 rounded">
                                                Edit
                                            </button>
                                        </Link>
                                        <button
                                            className="bg-red-500 text-white px-3 py-1 rounded"
                                            onClick={() => handleDelete(student.id)}
                                        >
                                            Delete
                                        </button>
                                    </td>
                                )}
                            </tr>
                        ))
                    ) : (
                        <tr>
                            <td colSpan={4} className="text-center p-4 text-gray-500">
                                No students found.
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
        </div>
    );
}
