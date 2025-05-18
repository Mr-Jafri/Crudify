import axios from "./axios";

interface Student {
  id?: number;
  fullName: string;
  email: string;
  age: number;
  dateodbirth: Date;
  phoneNumber: string;
}

export const getStudents = () => axios.get("/api/students");

export const addStudent = (student: Student) =>
  axios.post("/api/students", student);

export const deleteStudent = (id: number) =>
  axios.delete(`/api/students/delete/${id}`);

export const updateStudent = (id: number, student: Student) =>
  axios.put(`/api/students/${id}`, student);

export const getStudentById = (id: number) => axios.get(`/api/students/${id}`);
