import { Course } from './course';
 export interface Student {
    id: number;
    username: string;
    name: string;
    surname: string;
    photoURL: string;
    created: string;
    enrollmentDate: string;
    courses?: Course[];
}
