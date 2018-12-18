import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BsDropdownModule, TabsModule, PaginationModule, ModalModule } from 'ngx-bootstrap';
import { JwtModule } from '@auth0/angular-jwt';

import { AppComponent } from './app.component';
import { CourseListComponent } from './courses/course-list/course-list.component';
import { NavComponent } from './nav/nav.component';
import { AuthService } from './_services/auth.service';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { AlertifyService } from './_services/alertify.service';
import { appRoutes } from './routes';
import { AuthGuard } from './_guards/auth.guard';
import { WelcomeComponent } from './welcome/welcome.component';
import { StudentService } from './_services/student.service';
import { StudentListComponent } from './students/student-list/student-list.component';
import { StudentCardComponent } from './students/student-card/student-card.component';
import { StudentDetailComponent } from './students/student-detail/student-detail.component';
import { StudentCourseListComponent } from './students/student-course-list/student-course-list.component';
import { StudentEditComponent } from './students/student-edit/student-edit.component';
import { StudentEditResolver } from './_resolvers/member-edit.resolver';
import { PreventUnsavedChanges } from './_guards/prevent-unsaved-changes.guard';
import { CourseService } from './_services/course.service';
import { UserCourseListComponent } from './students/user-course-list/user-course-list.component';
import { CourseComponent } from './courses/course/course.component';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { HasRoleDirective } from './_directives/hasRole.directive';
import { AdminService } from './_services/admin.service';
import { RolesModalComponent } from './admin/roles-modal/roles-modal.component';
import { UserManagementComponent } from './admin/user-management/user-management.component';
import { CourseManagementComponent } from './admin/course-management/course-management.component';
import { CourseInstructorModalComponent } from './admin/course-instructor-modal/course-instructor-modal.component';
import { InstructorCourseListComponent } from './students/instructor-course-list/instructor-course-list.component';
import { InstructorCourseManagementComponent } from './admin/instructor-course-management/instructor-course-management.component';
import { InstructorCourseManagementListComponent } from './admin/instructor-course-management-list/instructor-course-management-list.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';

export function tokenGetter() {
    return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      CourseListComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      WelcomeComponent,
      StudentListComponent,
      StudentCardComponent,
      StudentDetailComponent,
      StudentCourseListComponent,
      StudentEditComponent,
      UserCourseListComponent,
      CourseComponent,
      AdminPanelComponent,
      HasRoleDirective,
      UserManagementComponent,
      RolesModalComponent,
      CourseManagementComponent,
      CourseInstructorModalComponent,
      InstructorCourseListComponent,
      InstructorCourseManagementComponent,
      InstructorCourseManagementListComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      BsDropdownModule.forRoot(),
      TabsModule.forRoot(),
      PaginationModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      ModalModule.forRoot(),
      JwtModule.forRoot({
          config: {
            tokenGetter: tokenGetter,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/auth']
          }
      }),
      PaginationModule.forRoot(),
      BsDatepickerModule.forRoot()
   ],
   providers: [
      AuthService,
      AlertifyService,
      AuthGuard,
      StudentService,
      StudentEditResolver,
      PreventUnsavedChanges,
      CourseService,
      AdminService
   ],
   entryComponents: [
      RolesModalComponent,
      CourseInstructorModalComponent
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
