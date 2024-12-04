// Given a 3d position as a seed, compute an even smoother procedural noise
// value. "Improving Noise" [Perlin 2002].
//
// Inputs:
//   st  3D seed
// Values between  -½ and ½ ?
//
// expects: random_direction, improved_smooth_step
float improved_perlin_noise( vec3 st) 
{
  /////////////////////////////////////////////////////////////////////////////
  // integer and fractional part
  vec3 i = floor(st);
  vec3 f = fract(st);

  // generate random gradients at corners
  vec3 g[8];
  g[0] = random_direction(i + vec3(0.0, 0.0, 0.0));
  g[1] = random_direction(i + vec3(1.0, 0.0, 0.0));
  g[2] = random_direction(i + vec3(0.0, 1.0, 0.0));
  g[3] = random_direction(i + vec3(1.0, 1.0, 0.0));
  g[4] = random_direction(i + vec3(0.0, 0.0, 1.0));
  g[5] = random_direction(i + vec3(1.0, 0.0, 1.0));
  g[6] = random_direction(i + vec3(0.0, 1.0, 1.0));
  g[7] = random_direction(i + vec3(1.0, 1.0, 1.0));

  // calculate displacement vectors
  vec3 d[8];
  d[0] = f - vec3(0.0, 0.0, 0.0);
  d[1] = f - vec3(1.0, 0.0, 0.0);
  d[2] = f - vec3(0.0, 1.0, 0.0);
  d[3] = f - vec3(1.0, 1.0, 0.0);
  d[4] = f - vec3(0.0, 0.0, 1.0);
  d[5] = f - vec3(1.0, 0.0, 1.0);
  d[6] = f - vec3(0.0, 1.0, 1.0);
  d[7] = f - vec3(1.0, 1.0, 1.0);

  // compute dot projects for each corner
  float dot_prods[8];
  for (int i = 0; i < 8; i++) {
	dot_prods[i] = dot(g[i], d[i]);
  }

  // smooth step 
  vec3 u = improved_smooth_step(f);

  // interpolation on x axis
  float x0 = mix(dot_prods[0], dot_prods[1], u.x);
  float x1 = mix(dot_prods[2], dot_prods[3], u.x);
  float x2 = mix(dot_prods[4], dot_prods[5], u.x);
  float x3 = mix(dot_prods[6], dot_prods[7], u.x);

  // on y axis
  float y0 = mix(x0, x1, u.y);
  float y1 = mix(x2, x3, u.y);

  // final noise
  return mix(y0, y1, u.z);
  /////////////////////////////////////////////////////////////////////////////
}

