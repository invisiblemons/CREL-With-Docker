import { AbstractControl, ValidatorFn } from "@angular/forms";

export class StringValidator {
  static confirmedPassword(newPassword: string): ValidatorFn {
    return (control: AbstractControl): { [key: string]: boolean } | null => {
      if (
        control.value &&
        control.value !== control.parent.get(newPassword).value
      ) {
        return { passwordNotMatch: true };
      }
      return null;
    };
  }

  static maxWards(wards): ValidatorFn {
    return (control: AbstractControl): { [key: string]: boolean } | null => {
      if (control.value && control.parent.get(wards).value.length) {
        if (control.value && control.value.length + control.parent.get(wards).value.length > 20)
          return { maxWards: true };
        return null;
      } else {
        if (control.value && control.value.length > 20)
          return { maxWards: true };
        return null;
      }
    };
  }

  static invalid(): ValidatorFn {
    
    return (control: AbstractControl): { [key: string]: boolean } | null => {
      return { invalid: true };
    };
  }
}
