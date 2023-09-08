import { Subject, debounceTime, distinctUntilChanged, filter, switchMap, take, tap, takeUntil, takeLast, last } from 'rxjs';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { CoursesService } from '../../services/courses.service';
import { PaginationReuslt, SearchCourseReponse } from 'src/app/core/api';
import { PageEvent } from '@angular/material/paginator';
import { FormBuilder, FormGroup } from '@angular/forms';
import { FormListCourses } from '../../models/form-list.model';
import { CourseItem } from '../../models/course-item.model';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit, OnDestroy {
  private readonly ngUnsubscribe$: Subject<void> = new Subject<void>();
  public items!: CourseItem[];
  public paginationResult!: PaginationReuslt<SearchCourseReponse>;
  public pageEvent: PageEvent;
  public formSearch!: FormGroup<FormListCourses>;

  constructor(private coursesService: CoursesService,
    private formBuilder: FormBuilder) {
      this.pageEvent = {
        length: 10,
        pageIndex: 0,
        pageSize: 10
      };
  }

  public ngOnInit(): void {
    this.formSearch = this.formBuilder.group<FormListCourses>(new FormListCourses());
    this.searchCourses(this.pageEvent);
    this.nameChanges();
  }

  public searchCourses(event: PageEvent) {
    this.pageEvent = event;
    this.coursesService.searchUsers(event.pageIndex, event.pageSize)
      .pipe(take(1))
      .subscribe((response) => {
        this.paginationResult = response.content;
        this.items = response.content.items;
      });
  }
  
  private nameChanges(): void {
    this.formSearch.controls.name.valueChanges.pipe(
      // vai fazer uma requisição a cada 1000 mls/ 1s não a cada interpolação do usuário
      debounceTime(1000),
      tap(console.log),
      // se  o maluco nem fez nada que se foda 
      filter(valor => valor != undefined),
      // Não deixa replicar pra um mesmo valor ou seja refazer uma nova chamada sem motivo
      distinctUntilChanged(),
      // cada vez ele abre uma nova subscribe ele não fica segurando ele e perfeito para esses casos de digitação que o que foi antes não faz diferença é só o agora
      // se for salvar usar merge map se não você pode perder e se lascar 
      switchMap(query => {
          return this.coursesService.searchUsers(this.pageEvent.pageIndex, this.pageEvent.pageSize, query == '' ? null : query)
      }),
      takeUntil(this.ngUnsubscribe$)
    )
    .subscribe((response) => {
      this.paginationResult = response.content;
      this.items = response.content.items;
    });
  }

  public search() {
    this.searchCourses(this.pageEvent);
  }

  public ngOnDestroy(): void {
    // Emit a value so that takeUntil will handle the closing of our subscriptions;
    this.ngUnsubscribe$.next();
    // Unsubscribe from our unsubscriber to avoid creating a memory leak
    this.ngUnsubscribe$.unsubscribe();
  }
}
