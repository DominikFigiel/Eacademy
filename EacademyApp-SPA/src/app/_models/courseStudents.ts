import { Course } from './course';

export interface CourseStudents {
    id: number;
    courseId: number;
    studentId: number;
    course: Course;
}
