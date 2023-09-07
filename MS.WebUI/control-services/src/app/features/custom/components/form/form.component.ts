import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { FormCustom } from '../../models/custom.model';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss']
})
export class FormComponent implements OnInit {
  public formRegister!: FormGroup<FormCustom>;
  public constructor(private formBuilder: FormBuilder) {

  }
  ngOnInit(): void {
    this.formRegister = this.formBuilder.group<FormCustom>(new FormCustom());
  }


  public addItem(): void {
    this.formRegister.controls.items.push(FormCustom.AddItem());
  }

  public removeItem(i: number): void {
    if(i !== 0)
      this.formRegister.controls.items.removeAt(i);
  }

  public save(): void {
    alert(JSON.stringify(this.formRegister.value));
  }
}
