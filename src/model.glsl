// Construct the model transformation matrix. The moon should orbit around the
// origin. The other object should stay still.
//
// Inputs:
//   is_moon  whether we're considering the moon
//   time  seconds on animation clock
// Returns affine model transformation as 4x4 matrix
//
// expects: identity, rotate_about_y, translate, PI
mat4 model(bool is_moon, float time)
{
  /////////////////////////////////////////////////////////////////////////////
  // Replace with your code 
  if (is_moon) {
	float theta = 0.5 * M_PI * time; // 2 pi radians per 4 sec
	mat4 R = rotate_about_y(theta); // rotation matrix
	mat4 S = uniform_scale(0.3); // scale matrix to 0.3 (70% smaller)
	mat4 T = translate(vec3(2, 0, 0)); // shift away from origin by 2 units
	return R * T * S; // first apply scale and translation, then rotate
  }
  else {
	return identity();
  }
  /////////////////////////////////////////////////////////////////////////////
}
