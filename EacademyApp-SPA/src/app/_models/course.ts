import { Student } from './student';
import { Module } from './module';
import { CourseStudents } from './courseStudents';

export interface Course {
    id: number;
    name: string;
    description: string;
    courseStudents?: CourseStudents[];
    modules?: Module[];
    instructor?: Student;
}
