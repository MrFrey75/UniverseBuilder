export interface Universe {
  id: string;
  name: string;
  description: string;
  createdDate: string;
  modifiedDate: string;
}

export interface CreateUniverseDto {
  name: string;
  description: string;
}

export interface UpdateUniverseDto {
  name: string;
  description: string;
}
