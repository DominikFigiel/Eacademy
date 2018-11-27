import { Student } from './student';
import { Module } from './module';
 export interface Course {
    id: number;
    name: string;
    description: string;
    students?: Student[];
    modules?: Module[];
}