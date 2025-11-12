const BASE_URL = import.meta.env.VITE_API_URL ?? '/api';

function resolve(url: string) {
  return url.startsWith('http') ? url : `${BASE_URL}${url}`;
}

async function request<T>(url: string, init?: RequestInit): Promise<T> {
  const token = localStorage.getItem('token'); // or wherever you store it
  const res = await fetch(resolve(url), {
    ...init,
    headers: {
      'Content-Type': 'application/json',
      ...(token ? { Authorization: `Bearer ${token}` } : {}),
      ...init?.headers,
    },
  });

  if (!res.ok) {
    const text = await res.text().catch(() => '');
    throw new Error(`${res.status} ${res.statusText} on ${url}: ${text}`);
  }
  // 204 No Content support
  if (res.status === 204) return undefined as T;
  return res.json() as Promise<T>;
}

export const http = {
  get: <T>(url: string) => request<T>(url),
  post: <T>(url: string, body?: unknown) =>
    request<T>(url, { method: 'POST', body: JSON.stringify(body) }),
  put:  <T>(url: string, body?: unknown) =>
    request<T>(url, { method: 'PUT', body: JSON.stringify(body) }),
  del:  <T>(url: string) =>
    request<T>(url, { method: 'DELETE' }),
};
