import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CoursesService } from '../../services/courses.service';
import { FormCourse } from '../../models/form-course.model';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent implements OnInit {
  public formCourses!: FormGroup<FormCourse>;

  constructor(private coursesService: CoursesService,
      private formBuilder: FormBuilder) {
  }

  public ngOnInit(): void {
    this.formCourses = this.formBuilder.group<FormCourse>(new FormCourse(), {
      validators: [ FormCourse.customValidatorDatesInterval() ]
    });
  }

  public saveCourse(): void {
    console.log(this.formCourses.value);
  }
}
