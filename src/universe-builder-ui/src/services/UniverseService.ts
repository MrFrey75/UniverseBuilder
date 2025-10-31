import { Universe, CreateUniverseDto, UpdateUniverseDto } from '../types/Universe';

const API_BASE_URL = 'http://localhost:5022/api';

export class UniverseService {
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

  static async getAll(): Promise<Universe[]> {
    const response = await fetch(`${API_BASE_URL}/Universe`);
    return this.handleResponse<Universe[]>(response);
  }

  static async getById(id: string): Promise<Universe> {
    const response = await fetch(`${API_BASE_URL}/Universe/${id}`);
    return this.handleResponse<Universe>(response);
  }

  static async create(data: CreateUniverseDto): Promise<Universe> {
    const response = await fetch(`${API_BASE_URL}/Universe`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    });
    return this.handleResponse<Universe>(response);
  }

  static async update(id: string, data: UpdateUniverseDto): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/Universe/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    });
    return this.handleResponse<void>(response);
  }

  static async delete(id: string): Promise<void> {
    const response = await fetch(`${API_BASE_URL}/Universe/${id}`, {
      method: 'DELETE',
    });
    return this.handleResponse<void>(response);
  }
}
