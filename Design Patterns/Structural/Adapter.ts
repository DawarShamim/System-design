// Also Known As Wrapper
//fixes an incompatibility that already exists. it's applied after the fact to make two things work together.
interface IMediaPlayer {
    play(fileName: string): void
    pause(): void
    stop(): void
}

class VLCPlayer {
    openFile(path: string): void { console.log(`[VLC] Opening: ${path}`) }
    startPlayback(): void { console.log(`[VLC] Playback started`) }
    suspendPlayback(): void { console.log(`[VLC] Playback suspended`) }
    closeFile(): void { console.log(`[VLC] File closed`) }
}

class QuickTimePlayer {
    loadMedia(url: string): void { console.log(`[QuickTime] Loading: ${url}`) }
    pressPlay(): void { console.log(`[QuickTime] Playing`) }
    pressPause(): void { console.log(`[QuickTime] Paused`) }
    eject(): void { console.log(`[QuickTime] Media ejected`) }
}

class VLCAdapter implements IMediaPlayer {
    private vlc: VLCPlayer

    constructor() { this.vlc = new VLCPlayer() }

    play(fileName: string): void {
        this.vlc.openFile(fileName)
        this.vlc.startPlayback()
    }

    pause(): void { this.vlc.suspendPlayback() }

    stop(): void { this.vlc.closeFile() }
}

class QuickTimeAdapter implements IMediaPlayer {
    private qt: QuickTimePlayer

    constructor() { this.qt = new QuickTimePlayer() }

    play(fileName: string): void {
        this.qt.loadMedia(fileName)
        this.qt.pressPlay()
    }

    pause(): void { this.qt.pressPause() }

    stop(): void { this.qt.eject() }
}

class AudioSystem {
    private player: IMediaPlayer

    constructor(player: IMediaPlayer) {
        this.player = player
    }

    setPlayer(player: IMediaPlayer): void { this.player = player }

    playTrack(file: string): void {
        console.log(`AudioSystem: playing "${file}"`)
        this.player.play(file)
    }

    pauseTrack(): void {
        console.log(`AudioSystem: pausing`)
        this.player.pause()
    }

    stopTrack(): void {
        console.log(`AudioSystem: stopping`)
        this.player.stop()
    }
}

// ── USAGE ────────────────────────────────────────────────────

const audio = new AudioSystem(new VLCAdapter())
audio.playTrack("song.mp4")
audio.pauseTrack()
audio.stopTrack()

// Swap to QuickTime — client code is UNCHANGED
audio.setPlayer(new QuickTimeAdapter())
audio.playTrack("podcast.mov")
audio.stopTrack()
