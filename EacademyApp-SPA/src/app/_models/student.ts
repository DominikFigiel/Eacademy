import { Course } from './course';
import { CourseStudents } from './courseStudents';

export interface Student {
    id: number;
    username: string;
    name: string;
    surname: string;
    photoURL: string;
    created: string;
    enrollmentDate: string;
    courseStudents?: CourseStudents[];
    roles?: string[];
    isInstructor?: boolean;
}
