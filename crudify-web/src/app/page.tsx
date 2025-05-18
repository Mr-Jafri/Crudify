'use client'

import Link from 'next/link'

export default function Home() {
  return (
    <main className="flex flex-col items-center justify-center min-h-screen p-4">
      <h1 className="text-4xl font-bold mb-6">Student Management</h1>
      <div className="space-x-4">
        <Link href="/students">
          <button className="bg-blue-500 text-white px-6 py-2 rounded hover:bg-blue-600">Student List</button>
        </Link>
        <Link href="/students/add">
          <button className="bg-green-500 text-white px-6 py-2 rounded hover:bg-green-600">Add Student</button>
        </Link>
      </div>
    </main>
  )
}