export class Admin {
  id: number;
  userName: string;
  password: string;
  name: string;
  birthday: Date;
  gender: boolean;
  email: string;
  phoneNumber: string;
  status: number;
  avatarFileId: null;
  avatarLink: null;
  token: string;
  exp: Date;

  constructor(res) {
    this.userName = null;
    this.password = null;
    this.name = null;
    this.birthday = null;
    this.gender = null;
    this.email = null;
    this.phoneNumber = null;
    this.status = 1;
    this.avatarFileId = null;
    this.avatarLink = null;
  }
}
