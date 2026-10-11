# Generated Asset Manifest — JANTAR MANTAR RUN

These assets have been generated or claimed in the connected PINOC library. They are **not yet integrated into the game repository**.

## Runner character
- Display name: **Man with Backpack**
- Asset ID: `26e1e01a-229d-482d-a41c-5a4c93761bb8`
- Format: VIGGCHAR Gaussian-splat character; high tier is about 1.3 MB / 48,000 splats.
- Runtime requirement: Viggle Splat Engine. This is not a conventional FBX/GLB mesh.
- PINOC app: https://viggle.ai/pinoc/app
- Unity plugin/instructions: https://viggle.ai/pinoc/app/plugin

## Delhi environment
- Display name: **Jantar Mantar Run — Delhi Street Environment**
- Asset ID: `33cab3aa-eacb-4c77-b6e7-79f1d66895ce`
- Format: normalised SPZ Gaussian-splat scene (one unit = one metre; ground at origin).
- Runtime requirement: compatible splat renderer / Viggle Splat Engine. This is not a conventional Unity mesh scene.
- PINOC app: https://viggle.ai/pinoc/app

## Animation library assets
These free library clips were claimed into the PINOC library:

| Clip | Asset ID |
|---|---|
| Running | `32c1d7ba-29d6-466c-a2bc-56ff17ca4d1a` |
| Sprinting Forward | `a075443a-1b3c-45d9-a3c5-6b6296d21bd1` |
| Run Jump | `92b846c7-a636-45cd-96d5-f34cc0e97be7` |
| Vault Over Obstacle | `91c3478c-e380-40fc-bd8c-25f9b883cb11` |
| Jump | `08872e23-d695-493d-8c03-cb595cd55e9c` |

## Important integration notes
- Download links from PINOC are signed and expire. Retrieve fresh URLs from the PINOC app/API when needed; do not embed temporary URLs in the game.
- For a web splat runtime, PINOC's guidance names `@viggle/splat-engine` with `playcanvas@2.18.0`. Unity requires the Viggle Splat Engine package.
- Character splat animation needs compatible splat-skeleton exports, not the ordinary Mixamo GLB alone.
- Before choosing this route for an Android endless runner, benchmark mobile performance and verify lane movement, jump/vault timing, camera tracking, collision logic, and UI.
- Do not claim the game is playable until a runtime project and tested build exist.
