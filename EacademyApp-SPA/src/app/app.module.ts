import { RouterModule } from '@angular/router';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { BsDropdownModule, TabsModule } from 'ngx-bootstrap';

import { AppComponent } from './app.component';
import { CourseComponent } from './course/course.component';
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

@NgModule({
   declarations: [
      AppComponent,
      CourseComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      WelcomeComponent,
      StudentListComponent,
      StudentCardComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      BsDropdownModule.forRoot(),
      TabsModule.forRoot(),
      RouterModule.forRoot(appRoutes)
   ],
   providers: [
      AuthService,
      AlertifyService,
      AuthGuard,
      StudentService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
