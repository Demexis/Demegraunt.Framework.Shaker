[![GitHub Release](https://img.shields.io/github/v/release/Demexis/Demegraunt.Framework.Shaker.svg)](https://github.com/Demexis/Demegraunt.Framework.Shaker/releases/latest)
[![MIT license](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)
# Demegraunt.Framework.SpriteAnimations

Position shaker. Contains ShakerTrack for Timeline package.

## Table of Contents
- [Setup](#setup)
  - [Requirements](#requirements)
  - [Installation](#installation)
  - [Usage](#usage)

## Setup

### Requirements

* Unity 2022.2 or later

### Installation

Use __ONE__ of two options:

#### a) Unity Package (Recommended)
Download a unity package from [the latest release](../../releases).

#### b) Package Manager
1. Open Package Manager from Window > Package Manager.
2. Click the "+" button > Add package from git URL.
3. Enter the following URL:
```
https://github.com/Demexis/Demegraunt.Framework.Shaker.git
```

Alternatively, open *Packages/manifest.json* and add the following to the dependencies block:

```json
{
    "dependencies": {
        "com.demegraunt.framework.shaker": "https://github.com/Demexis/Demegraunt.Framework.Shaker.git"
    }
}
```

### Usage

Attach the `ShakerComponent` component to a game-object. Check the `bool ShakeAlways` or call `Shake(float time)` manually in code or via UnityEvent(-s).

---
Add `ShakeTrack` to your timeline. Set the "shaking transform". RMB, click *Create -> ShakeClip*, configure parameters.
