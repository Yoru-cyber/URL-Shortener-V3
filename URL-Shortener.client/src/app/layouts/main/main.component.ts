import { Component } from '@angular/core';
import { NzContentComponent, NzFooterComponent, NzHeaderComponent, NzLayoutModule } from 'ng-zorro-antd/layout';
import { HeaderComponent } from '../../components/header/header.component';
import { FooterComponent } from '../../components/footer/footer.component';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [NzLayoutModule, NzHeaderComponent, NzContentComponent, NzFooterComponent, HeaderComponent, FooterComponent ],
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent {}
