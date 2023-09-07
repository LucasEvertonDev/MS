import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { authGuard } from './core/guards/authorization/auth.guard';
import { unauthorizationGuard } from './core/guards/unauthorization/unauthorization.guard';

const routes: Routes = [
  {
    path: 'home',
    loadChildren: () => import('./features/home/home.module').then(m => m.HomeModule),
    canActivate: [authGuard]
  },
  {
    path: 'auth',
    loadChildren: () => import('./features/auth/auth.module').then(m => m.AuthModule),
  },
  {
    path: 'courses',
    loadChildren: () => import('./features/courses/courses.module').then(m => m.CoursesModule),
    canActivate: [authGuard]
  },
  {
    path: 'custom',
    loadChildren: () => import('./features/custom/custom.module').then(m => m.CustomModule),
    canActivate: [authGuard]
  },
  {
    path: '**',
    redirectTo: 'home',
    pathMatch: 'full'
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
