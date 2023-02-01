This is a Demo for PinguJumper, which was created as a group project durring ÜEiGE 22/23
by 
Claire-Estelle Böhm-Pélissier du Besset,
Benedikt Stefan Günther,
Korbinian Burkhardt,
Leon Muschew


Controls:

	Esc: Settings
	W/A/S/D: Run
	Spacebar: Jump
	Mouse: Look
	Ctrl + G: God-Mode
	God-Mode:
		Crtl: down
		Spacebar: up
	Antarctic 1:
		Hold P at the Start of the Level: Force to show "play.txt" (in Antarctic_Level/) no matter how fast it is (only needed to play a specific Replay)




Each team member created:


Benedikt:

	Assets:

	Level:
	-Antartic 1 (Level)
	-Main Hub

	Scripts:
	-GhostAnimation
	-GhostPlayer
	-LoadSceneOnTrigger
	-LostPinguAnimation
	-PinguSlide
	-PinguSlip
	-PlayerAnimation
	-WaterMovement
	-WonLevel

	Models:
	-pingu
	-Iceberg
	-IceAndSnow (Plattform)
	-Hub Plattform
	-Sign
	
	Textures:
	-All AI generated Textures (with Stable Diffusion 1.5)
	-Pingu texture (& variants)
	-IceAndSnowTexture
	-Cracked Ice Texture

	Animations:
	-LostPingu Animation (controller)
	-PinguWalkAnimator (controller)



	Features:

	- Replay /Ghost-Player feature (Antarctic 1)
	- Sliding & Slipping
	- Animation



Claire-Estelle:

	Level:
	-Antartic 2

	Scripts:
	-DeathZone
	-GeneigtePlattform
	-IceBox
	-IceCube
	-IceCubePlattforms
	-MovingPlattform
	-in PlayerBehavior:
	nur die Abschnitte, die mit den oben genannten Skripte interagieren
	(changeSpawnPoint() und Teile von Spawn(),OnCollisionEnter(),OnTriggerEnter(),OnTriggerExit())

	Features:
	-verstecktes Teil-Level
	-invisible platforms
	-verschiedene Spawnpoints



Korbinian:

	Level:
	-Cave

	Assets:
	-Alle Assets im Cave Level
	-eine Platform im Antarctic level

	Features:
	-Alle Features von Cave Level:
		-prisma mechanik
		-Dynamische Szenen Belichtung
		-Plattform an die Richtige stelle schieben (plat. snapps)
	
	-Save System
	-PlayerBehaviour (movement und Kamera)
	-Mouse Sensitivity Settings
	-God Mode



Leon:

	Level:
	-Space

	Scripts:
	-Player Life
	-Collect
	-Waypoint Mov
	-Rotate
	-makeVisible

	Features:
	-Collecting Fish
	-Black Hole
	-Aliens (Enemies)




Used Assets:

	Animations:
	-https://www.mixamo.com/#/?limit=96&page=1&query=Standard+Walk  (Walking Animation)
	-https://www.mixamo.com/#/?page=1&query=Happy+Idle   (Happy Idle Animation)
	-https://www.mixamo.com/#/?page=1&query=sad+Idle  (Sad Idle Animation)
	-https://www.mixamo.com/#/?page=1&query=exited  (Exited Animation)

	Skybox:
	-https://assetstore.unity.com/packages/2d/textures-materials/sky/customizable-skybox-174576  (Skybox for Antarctic 1 & 2 & Main Hub)
	-https://assetstore.unity.com/packages/2d/textures-materials/sky/spaceskies-free-80503	(Skybox for Space)

	Textures:
	-https://assetstore.unity.com/packages/2d/textures-materials/water/stylize-water-texture-153577  (Water Texture for Antarctic 1 & Main Hub)
	-https://assetstore.unity.com/packages/2d/textures-materials/alien-floor-6023  (Alien Platform Texture)
	
	Models:
	-https://assetstore.unity.com/packages/3d/characters/animals/fish/fish-polypack-202232 (Fish Model)
	-https://assetstore.unity.com/packages/3d/vehicles/space/alien-ships-pack-131137  (Alien Spaceship Models)

	Sound:
	-https://assetstore.unity.com/packages/audio/music/free-music-tracks-for-games-156413  (Space Background Music)
	-https://assetstore.unity.com/packages/audio/sound-fx/free-casual-game-sfx-pack-54116  (Fish Collection Sound)
	-https://pixabay.com/de/music/schone-stucke-please-calm-my-mind-125566/  (Trailer Music)

	Particles & Effects:
	-https://assetstore.unity.com/packages/tools/particles-effects/dark-singularity-156548 (Black Hole)
