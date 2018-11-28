import { AdminService } from './../../_services/admin.service';
import { Component, OnInit } from '@angular/core';
import { Student } from 'src/app/_models/student';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css']
})
export class UserManagementComponent implements OnInit {
  users: Student[];

  constructor(private adminSerivce: AdminService) { }

  ngOnInit() {
    this.getUsersWithRoles();
  }

  getUsersWithRoles() {
    this.adminSerivce.getUsersWithRoles().subscribe((users: Student[]) => {
      this.users = users;
    }, error => {
      console.log(error);
    });
  }

}
