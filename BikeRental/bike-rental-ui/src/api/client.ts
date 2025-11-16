/**
 * Base backend URL for all API requests.
 */
const BASE_URL = "http://localhost:8083";

/**
 * Ensures absolute URLs; relative paths are prefixed automatically.
 */
function resolve(url: string): string {
  return url.startsWith("http") ? url : `${BASE_URL}${url}`;
}

/**
 * Core request wrapper around Fetch API.
 * Handles:
 *  - JSON encoding
 *  - typed responses
 *  - auth token insertion
 *  - error propagation
 *  - 204 No Content
 */
async function request<T>(url: string, init?: RequestInit): Promise<T> {
  const token = localStorage.getItem("token");

  const res = await fetch(resolve(url), {
    ...init,
    headers: {
      "Content-Type": "application/json",
      ...(token ? { Authorization: `Bearer ${token}` } : {}),
      ...init?.headers, // allow override
    },
  });

  // Error handling with diagnostic details from server
  if (!res.ok) {
    const text = await res.text().catch(() => "");
    throw new Error(`${res.status} ${res.statusText} on ${url}: ${text}`);
  }

  // Support empty responses (DELETE, PUT)
  if (res.status === 204) {
    return undefined as T;
  }

  return res.json() as Promise<T>;
}

/**
 * Lightweight HTTP client abstraction.
 * Each method returns a typed Promise<T>.
 */
export const http = {
  /**
   * Perform a GET request.
   */
  get: <T>(url: string) => request<T>(url),

  /**
   * Perform a POST request with optional JSON body.
   */
  post: <T>(url: string, body?: unknown) =>
    request<T>(url, { method: "POST", body: JSON.stringify(body) }),

  /**
   * Perform a PUT request with optional JSON body.
   */
  put: <T>(url: string, body?: unknown) =>
    request<T>(url, { method: "PUT", body: JSON.stringify(body) }),

  /**
   * Perform a DELETE request.
   */
  del: <T>(url: string) =>
    request<T>(url, { method: "DELETE" }),
};
