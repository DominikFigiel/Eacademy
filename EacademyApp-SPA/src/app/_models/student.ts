import { Course } from './course';
<<<<<<< HEAD
import { CourseStudents } from './courseStudents';

export interface Student {
=======
 export interface Student {
>>>>>>> e351c261f616061baedf560f82083e5a5de553f6
    id: number;
    username: string;
    name: string;
    surname: string;
    photoURL: string;
    created: string;
    enrollmentDate: string;
<<<<<<< HEAD
    courseStudents?: CourseStudents[];
}
=======
    courses?: Course[];
}
>>>>>>> e351c261f616061baedf560f82083e5a5de553f6
