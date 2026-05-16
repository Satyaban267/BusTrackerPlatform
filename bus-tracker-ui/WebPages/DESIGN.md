# Design System Specification: The Digital Hearth

## 1. Overview & Creative North Star

**Creative North Star: "The Digital Hearth"**
In a rural context, technology should not feel like an alien imposition; it should feel like a trusted community member. This design system moves away from the cold, sterile "Silicon Valley" blue-and-white aesthetic in favor of **Editorial Ruralism**. We use intentional asymmetry, layered organic surfaces, and high-contrast typography to create an interface that feels as grounded as the earth and as reliable as the sunrise.

The system breaks the "template" look by treating the mobile screen as a canvas of stacked tactile materials. By utilizing overlapping elements—such as a bus status card partially floating over a landscape header—we create a sense of depth and physical presence. This non-traditional layout guides the eye toward the most critical information (the bus location) while maintaining a warm, non-intimidating atmosphere.

---

## 2. Colors

The palette is a sophisticated interpretation of the Indian landscape: Terracotta (`primary`), Deep Foliage Green (`secondary`), and Harvest Ochre (`tertiary`).

*   **Primary (Terracotta):** `#982400` – Used for primary actions and critical path markers. It provides a warm, urgent-but-inviting focal point.
*   **Secondary (Deep Green):** `#1b6d24` – Used to signify reliability, "on-time" status, and safety.
*   **Tertiary (Ochre):** `#803d00` – Used for highlights, warnings, or secondary information that requires a "human" touch.

### The "No-Line" Rule
Standard 1px borders are strictly prohibited for sectioning. They create visual noise and make the UI feel "boxed in." Instead, boundaries must be defined solely through background color shifts. Use `surface_container_low` (`#fff0ed`) to separate sections from the main `background` (`#fff8f6`).

### Surface Hierarchy & Nesting
Treat the UI as physical layers. An element's importance is defined by its "altitude" in the color stack:
1.  **Base:** `surface` (#fff8f6)
2.  **Sectioning:** `surface_container_low` (#fff0ed)
3.  **Interactive Cards:** `surface_container` (#ffe9e4)
4.  **Floating Modals:** `surface_container_highest` (#ffdad2)

### The "Glass & Gradient" Rule
To add a premium feel to a utility app, use Glassmorphism for floating navigation bars or "Live Tracking" headers. Apply a semi-transparent `surface` color with a `20px` backdrop blur. For main CTAs, use a subtle linear gradient from `primary` (#982400) to `primary_container` (#bf360c) to add "soul" and dimension.

---

## 3. Typography

The typography strategy balances high-end editorial flair with uncompromising legibility.

*   **Display & Headlines (Plus Jakarta Sans):** These use a modern, slightly geometric sans-serif to provide an authoritative yet friendly voice. `display-lg` (3.5rem) should be used sparingly for large, emotive welcome states or arrival times.
*   **Body & Labels (Public Sans):** A hardworking, neutral typeface. `body-lg` (1rem) is the workhorse for bus stop names and descriptions, ensuring that even in bright sunlight or on low-end screens, the text remains crisp.

**Hierarchy as Brand:** Use `headline-sm` for "Bus Numbers" to give them a "hero" status. Use `label-md` in all-caps with 5% letter spacing for metadata (e.g., "NEXT STOP") to create a refined, curated look.

---

## 4. Elevation & Depth

We avoid the "shadow-heavy" look of 2010s material design in favor of **Tonal Layering**.

*   **The Layering Principle:** Place a `surface_container_lowest` card on a `surface_container_low` background. The subtle shift in hex value creates a soft, natural lift that mimics fine paper.
*   **Ambient Shadows:** If a floating action button (FAB) or critical alert requires a shadow, use a large `40px` blur with 6% opacity. The shadow color should be a tint of `on_surface` (`#2b1611`), not pure black, to ensure it feels like natural ambient light.
*   **The "Ghost Border":** For high-accessibility needs, use a `1px` border using `outline_variant` (#e2bfb6) at **15% opacity**. It should be felt, not seen.
*   **Glassmorphism:** Use for persistent "Live Status" banners. A container with `surface_variant` at 80% opacity and a backdrop blur creates a "frosted glass" effect that keeps the user grounded in the map background while providing a legible surface for text.

---

## 5. Components

### Buttons
*   **Primary:** Pill-shaped (`rounded-full`), `primary` (#982400) fill, `on_primary` (#ffffff) text.
*   **Secondary:** `secondary_container` (#a0f399) fill with `on_secondary_container` (#217128) text.
*   **Styling:** Use `xl` (1.5rem) corner radius for a friendly, approachable touch.

### Input Fields
*   **Style:** No bottom lines. Use a `surface_container_high` (#ffe2db) filled box with `lg` (1rem) rounded corners.
*   **Focus State:** A `2px` "Ghost Border" using `primary` (#982400).

### Cards & Lists
*   **Rule:** **Zero Divider Lines.** Separate bus list items using `12px` of vertical white space and a subtle background shift to `surface_container_low`.
*   **The "Bus Card":** Use `surface_container` (#ffe9e4) for the card body. Use an asymmetrical `tertiary` (#803d00) accent tab on the left side to denote the bus category (Express vs. Local).

### Signature Component: The "Route Thread"
Instead of a standard list for bus stops, use a thick, organic vertical line in `secondary_fixed` (#a3f69c). Stops are represented by large `surface` circles. This literal, "map-like" visualization is easier to grasp for non-tech-savvy users than a text-only list.

---

## 6. Do’s and Don’ts

### Do
*   **DO** use high contrast. Ensure all text on `surface` backgrounds uses `on_surface` (#2b1611).
*   **DO** use literal iconography. A "Bus" icon should look like the local buses users see on the road, not a futuristic shuttle.
*   **DO** use generous spacing. Rural users may have larger fingers or be using the app while walking; targets should be at least `48px`.

### Don’t
*   **DON'T** use pure black (#000000) or pure white (#FFFFFF). Use the warm neutrals provided (`background`, `on_background`).
*   **DON'T** use 1px solid dividers. They clutter the "Digital Hearth" aesthetic.
*   **DON'T** use technical jargon like "ETA" or "GPS Signal." Use "Arriving in..." or "Bus Location Found."