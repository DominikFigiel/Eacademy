import { CourseComponent } from './courses/course/course.component';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { CourseListComponent } from './courses/course-list/course-list.component';
import { AuthGuard } from './_guards/auth.guard';
import { WelcomeComponent } from './welcome/welcome.component';
import { StudentListComponent } from './students/student-list/student-list.component';
import { StudentDetailComponent } from './students/student-detail/student-detail.component';
import { StudentEditComponent } from './students/student-edit/student-edit.component';
import { StudentEditResolver } from './_resolvers/member-edit.resolver';
import { UserCourseListComponent } from './students/user-course-list/user-course-list.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'welcome', component: WelcomeComponent},
            { path: 'courses', component: CourseListComponent},
            { path: 'courses/:id', component: CourseComponent},
            { path: 'students', component: StudentListComponent},
            { path: 'students/:id', component: StudentDetailComponent},
            { path: 'student/edit', component: StudentEditComponent,
                resolve: {student: StudentEditResolver}, canDeactivate: [PreventUnsavedChanges]},
            { path: 'student/courses', component: UserCourseListComponent},
            { path: 'admin', component: AdminPanelComponent, data: {roles: ['Admin', 'Instructor']}}
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'}
];
