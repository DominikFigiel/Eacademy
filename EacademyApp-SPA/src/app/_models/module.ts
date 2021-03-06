import { Course } from './course';

export interface Module {
    id: number;
    name: string;
    description: string;
    date: string;
    hasFileAttachment: boolean;
    assignmentName: string;
    hasAssignment: boolean;
    course: Course;
}
