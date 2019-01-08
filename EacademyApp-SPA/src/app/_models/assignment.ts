import { Student } from "./student";
import { Module } from "./module";

export interface Assignment {
  id: number;
  grade?: number;
  student?: Student;
  studentId?: number;
  module?: Module;
  moduleId?: number;
}
