export interface Location {
  id: string;
  universeId: string;
  parentLocationId?: string;
  name: string;
  type: string;
  description: string;
  coordinates?: LocationCoordinates;
  climate: string;
  population?: number;
  area?: number;
  customProperties: Record<string, string>;
  tags: string[];
  createdDate: string;
  modifiedDate: string;
}

export interface LocationCoordinates {
  latitude: number;
  longitude: number;
  altitude?: number;
}

export interface CreateLocationDto {
  universeId: string;
  parentLocationId?: string;
  name: string;
  type: string;
  description: string;
  coordinates?: LocationCoordinates;
  climate: string;
  population?: number;
  area?: number;
  customProperties: Record<string, string>;
  tags: string[];
}

export interface UpdateLocationDto {
  parentLocationId?: string;
  name: string;
  type: string;
  description: string;
  coordinates?: LocationCoordinates;
  climate: string;
  population?: number;
  area?: number;
  customProperties: Record<string, string>;
  tags: string[];
}

export interface LocationTreeNode extends Location {
  children: LocationTreeNode[];
  level: number;
}
