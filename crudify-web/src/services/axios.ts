// import axios from 'axios'

// const instance = axios.create({
//   baseURL: 'https://localhost:7106/api', // backend base URL
// })

// instance.interceptors.request.use(
//   (config) => {
//     debugger;
//     // Get token from localStorage or sessionStorage
//     const token = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJZCI6IjhkNGEzYTdhLWQxOWQtNGY4ZS1iYTNiLTFhZWI4MTdlMWRhZSIsImVtYWlsIjoic2phZnJpQGNhbHJvbS5jb20iLCJzdWIiOiJzamFmcmlAY2Fscm9tLmNvbSIsImp0aSI6IjQ2ZmIwNjc3LWFlZTUtNGY4Ni04MzZjLThhZTRkMmZjNzkzZiIsInJvbGUiOiJBZG1pbiIsIm5iZiI6MTc0NzQ3ODU4MCwiZXhwIjoxNzQ3NDgwMzgwLCJpYXQiOjE3NDc0Nzg1ODB9.NADehzsAlhJ3Xh2bmCWSyc7lPQmu8u3kg14TkFRO89g'  //// typeof window !== 'undefined' ? localStorage.getItem('token') : null

//     if (token && config.headers) {
//       config.headers['Authorization'] = `Bearer ${token}`;
//       config.headers['charset'] = 'utf-8';
//       config.headers['accept'] = 'application/json';
//       config.headers['contentType'] = 'application/json';

//     }

//     return config
//   },
//   (error) => Promise.reject(error)
// )

// instance.interceptors.response.use(
//   (response) => {
//     debugger;
//     const test = response;
//     return response.data
//   },
//   (error) => {
//     console.error('API error:', error.response?.data || error.message)
//     return Promise.reject(error)
//   }
// )

// export default instance

// --rotate-180

import axios from "axios";

// import { toast } from "src/components/snackbar";

import { JWT_STORAGE_KEY } from "@/auth/context/jwt";

// ----------------------------------------------------------------------

axios.defaults.baseURL = process.env.NEXT_PUBLIC_SERVER_URL;

// ----------------------------------------------------------------------

axios.interceptors.request.use(
  (req) => {
    return req;
  },
  (error) => Promise.reject(new Error(error.message ?? error))
);

axios.interceptors.response.use(
  (res) => res,
  (error) => {
    // toast.error(error.response?.data.message || "Something went wrong!");

    if (error.response?.status === 401) {
      sessionStorage.removeItem(JWT_STORAGE_KEY);
    }
    return error.response;
  }
);

// ----------------------------------------------------------------------

export default axios;

export const endpoints = {
  auth: {
    loggedUser: "/auth/loggedUser",
    signIn: "/api/auth/login",
  },
  student: {
    list: "/students",
    details: "/students/:id",
    delete: "/students/delete/:id",
    create: "/students",
  },
};
