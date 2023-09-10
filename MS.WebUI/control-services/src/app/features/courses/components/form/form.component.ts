import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CoursesService } from '../../services/courses.service';
import { FormCourse } from '../../models/form-course.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent implements OnInit {
  public formCourses!: FormGroup<FormCourse>;
  public novaEntrada: boolean;

  constructor(private coursesService: CoursesService,
    private formBuilder: FormBuilder,
    private actvatedRouter: ActivatedRoute) {
    this.novaEntrada = !(this.actvatedRouter.snapshot.url[0].path === 'edit');
  }

  public ngOnInit(): void {
    this.formCourses = this.formBuilder.group<FormCourse>(new FormCourse(), {
      validators: [FormCourse.customValidatorDatesInterval()]
    });

    if (!this.novaEntrada) {
      this.formCourses.controls.id.setValue(this.actvatedRouter.snapshot.url[1].path);
    }
  }

  public saveCourse(): void {
    alert(JSON.stringify(this.formCourses.value));
  }
}
