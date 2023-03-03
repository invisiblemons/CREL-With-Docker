import { Ward } from "src/app/shared/models/ward.model";
import { AreaManager } from "../area-manager/area-manager.model";

export class Team {
  id: number;
  description: string;
  name: string;
  status: number;
  areaManagerTeams: AreaManagerTeam[];
  wards:            Ward[];

  constructor(res, isCreate) {
    if (res) {
      if (!isCreate) this.id = res.id;
      this.description = res.description ? res.description.trim() : "";
      this.name = res.name.trim();
      this.status = res.status;
    } else {
      this.description = null;
      this.name = null;
      this.status = 1;
      this.areaManagerTeams = null;
    }
  }
}

export class AreaManagerTeam {
  areaManagerId: number;
  endDate: Date;
  id: number;
  startDate: "Date";
  teamId: number;
  areaManager: AreaManager;
}
