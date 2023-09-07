import { AuthorizationService } from 'src/app/core/services/authorization.services';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-layout-nav-bar',
  templateUrl: './layout-nav-bar.component.html',
  styleUrls: ['./layout-nav-bar.component.scss']
})
export class LayoutNavBarComponent implements OnInit {
 
  constructor(private authorizationService: AuthorizationService,
    private router: Router,) {
  }

  public ngOnInit(): void {
  }

  public signOut(): void {
    this.authorizationService.logOut();
    this.router.navigateByUrl('/auth');
  }
}
