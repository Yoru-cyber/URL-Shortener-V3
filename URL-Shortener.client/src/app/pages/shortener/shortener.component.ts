import { Component, inject } from '@angular/core';
import { MainComponent } from '../../layouts/main/main.component';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import ShortenedUrlDto from '../../Dtos/ShortenedUrl';
import { UrlShortenerService } from '../../services/url-shortener.service';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzButtonModule } from 'ng-zorro-antd/button';
@Component({
  selector: 'app-shortener',
  standalone: true,
  imports: [MainComponent, ReactiveFormsModule, NzInputModule, NzButtonModule],
  templateUrl: './shortener.component.html',
  styleUrl: './shortener.component.css',
})
export class ShortenerComponent {
  private urlShortenerService = inject(UrlShortenerService);

  urlForm: FormGroup = new FormGroup({
    originalUrl: new FormControl('', [
      Validators.required,
      Validators.pattern('https?://.+'),
    ]),
  });
  shortenedUrlDto: ShortenedUrlDto | null = null;
  error: string | null = null;

  onSubmit(): void {
    if (this.urlForm.valid) {
      const originalUrl = this.urlForm.get('originalUrl')?.value;
      console.log(originalUrl);
      this.urlShortenerService.createShortenedUrl(originalUrl).subscribe({
        next: (response) => {
          console.log(response);
          this.shortenedUrlDto = response;
          this.error = null;
        },
        error: () => {
          this.error = 'Failed to shorten the URL. Please try again.';
          this.shortenedUrlDto = null;
        },
      });
    }
  }
}
