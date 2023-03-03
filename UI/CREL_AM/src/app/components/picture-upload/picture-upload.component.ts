import {
  Component,
  OnInit,
  Input,
  ViewChild,
  ElementRef,
  Output,
  EventEmitter,
  ChangeDetectorRef,
} from "@angular/core";
import {
  AVATAR_DEFAULT,
  IMAGE_DEFAULT,
} from "src/app/shared/constants/common.const";
import { Media } from "src/app/shared/models/media.model";

@Component({
  selector: "app-picture-upload",
  templateUrl: "./picture-upload.component.html",
  styleUrls: ["./picture-upload.component.scss"],
})
export class PictureUploadComponent implements OnInit {
  @Input() avatar: boolean;
  @Input() certificates: boolean;
  @Input() imgSrc: string;
  @Input() imgSrcArray: Media[];

  @Output() outputImg = new EventEmitter<File>();
  @Output() outputImgArray = new EventEmitter<File[]>();
  @Output() outputOldImgArray = new EventEmitter<Media[]>();
  @Output() outputCertificatesImgArray = new EventEmitter<File[]>();
  @Output() outputOldCertificatesImgArray = new EventEmitter<Media[]>();

  file: any = {};
  imgSrcPreviewUrl: any = {};
  @ViewChild("fileInput") fileInput: ElementRef;

  isChange: boolean = false;

  constructor(private ref: ChangeDetectorRef) {
    this.handleImageChange = this.handleImageChange.bind(this);
  }

  ngOnInit() {
    this.file = null;
    if (this.avatar) {
      this.imgSrcPreviewUrl = this.imgSrc
        ? this.imgSrc
        : this.avatar
        ? AVATAR_DEFAULT
        : IMAGE_DEFAULT;
    }
  }

  handleClick() {
    this.fileInput.nativeElement.click();
    setTimeout(() => {
      this.isChange = true;

      this.ref.detectChanges();
    }, 500);
  }
  handleRemoveAvatar() {
    this.file = null;
    this.imgSrcPreviewUrl =
      this.imgSrc !== undefined
        ? this.imgSrc
        : this.avatar
        ? AVATAR_DEFAULT
        : IMAGE_DEFAULT;
    this.fileInput.nativeElement.value = null;
  }

  handleImageChange(event) {
    if (this.avatar) {
      event.preventDefault();
      let reader = new FileReader();
      let file = event.target.files[0];
      reader.onloadend = () => {
        this.file = file;
        this.imgSrcPreviewUrl = reader.result;
      };
      this.outputImg.emit(file);
      reader.readAsDataURL(file);
    } else {
      if (this.certificates) {
        this.outputCertificatesImgArray.emit(event.currentFiles);
      } else {
        this.outputImgArray.emit(event.currentFiles);
      }
    }
  }

  remove(file) {
    this.imgSrcArray = this.imgSrcArray.filter((obj) => {
      return file.id !== obj.id;
    });
    if (this.certificates) {
      this.outputOldCertificatesImgArray.emit(this.imgSrcArray);
    } else {
      this.outputOldImgArray.emit(this.imgSrcArray);
    }
  }
}
