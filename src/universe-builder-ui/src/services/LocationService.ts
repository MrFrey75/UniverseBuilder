import { Location, CreateLocationDto, UpdateLocationDto } from '../types/Location';

const API_BASE_URL = 'http://localhost:5022/api';

export class LocationService {
  private static async handleResponse<T>(response: Response): Promise<T> {
    if (!response.ok) {
      const error = await response.json().catch(() => ({ message: 'An error occurred' }));
      throw new Error(error.message || `HTTP error! status: ${response.status}`);
    }
    
    if (response.status === 204) {
      return {} as T;
    }
    
    return response.json();
  }

  static async getAllByUniverse(universeId: string): Promise<Location[]> {
    const response = await fetch(`${API_BASE_URL}/Location/universe/${universeId}`);
    return this.handleResponse<Location[]>(response);
  }

  static async getRootLocations(universeId: string): Promise<Location[]> {
    const response = await fetch(`${API_BASE_URL}/Location/universe/${universeId}/roots`);
    return this.handleResponse<Location[]>(response);
  }

  static async getById(id: string): Promise<Location> {
    const response = await fetch(`${API_BASE_URL}/Location/${id}`);
    return this.handleResponse<Location>(response);
  }

  static async getChildren(id: string): Promise<Location[]> {
    const response = await fetch(`${API_BASE_URL}/Location/${id}/children`);
    return this.handleResponse<Location[]>(response);
  }

  static async getAncestors(id: string): Promise<Location[]> {
    const response = await fetch(`${API_BASE_URL}/Location/${id}/ancestors`);
    return this.handleResponse<Location[]>(response);
  }

  static async getPath(id: string): Promise<Location[]> {
    const response = await fetch(`${API_BASE_URL}/Location/${id}/path`);
    return this.handleResponse<Location[]>(response);
  }

  static async getSiblings(id: string): Promise<Location[]> {
    const response = await fetch(`${API_BASE_URL}/Location/${id}/siblings`);
    return this.handleResponse<Location[]>(response);
  }

  static async create(data: CreateLocationDto): Promise<Location> {
    const response = await fetch(`${API_BASE_URL}/Location`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    });
    return this.handleResponse<Location>(response);
  }

  static async update(id: string, data: UpdateLocationDto): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/Location/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    });
    return this.handleResponse<void>(response);
  }

  static async move(id: string, newParentId?: string): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/Location/${id}/move`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ newParentId }),
    });
    return this.handleResponse<void>(response);
  }

  static async delete(id: string): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/Location/${id}`, {
      method: 'DELETE',
    });
    return this.handleResponse<void>(response);
  }
}
