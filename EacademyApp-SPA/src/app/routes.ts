import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CourseComponent } from './course/course.component';
import { AuthGuard } from './_guards/auth.guard';
import { WelcomeComponent } from './welcome/welcome.component';
import { StudentListComponent } from './students/student-list/student-list.component';
<<<<<<< HEAD
import { StudentDetailComponent } from './students/student-detail/student-detail.component';
=======
>>>>>>> e351c261f616061baedf560f82083e5a5de553f6

export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'welcome', component: WelcomeComponent},
            { path: 'courses', component: CourseComponent},
<<<<<<< HEAD
            { path: 'students', component: StudentListComponent},
            { path: 'students/:id', component: StudentDetailComponent},
=======
            { path: 'student-list', component: StudentListComponent}
>>>>>>> e351c261f616061baedf560f82083e5a5de553f6
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'}
];