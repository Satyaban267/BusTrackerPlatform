## GitHub Copilot Chat

- Extension: 0.47.0 (prod)
- VS Code: 1.119.0 (8b640eef5a6c6089c029249d48efa5c99adf7d51)
- OS: win32 10.0.26200 x64
- GitHub Account: Satyaban267

## Network

User Settings:
```json
  "http.systemCertificatesNode": true,
  "github.copilot.advanced.debug.useElectronFetcher": true,
  "github.copilot.advanced.debug.useNodeFetcher": false,
  "github.copilot.advanced.debug.useNodeFetchFetcher": true
```

Connecting to https://api.github.com:
- DNS ipv4 Lookup: 20.207.73.85 (114 ms)
- DNS ipv6 Lookup: Error (72 ms): getaddrinfo ENOTFOUND api.github.com
- Proxy URL: None (1 ms)
- Electron fetch (configured): Error (3288 ms): Error: net::ERR_CONNECTION_TIMED_OUT
	at SimpleURLLoaderWrapper.<anonymous> (node:electron/js2c/utility_init:2:10684)
	at SimpleURLLoaderWrapper.emit (node:events:519:28)
	at SimpleURLLoaderWrapper.callbackTrampoline (node:internal/async_hooks:130:17)
  {"is_request_error":true,"network_process_crashed":false}
- Node.js https: 