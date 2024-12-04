// Set the pixel color using Blinn-Phong shading (e.g., with constant blue and
// gray material color) with a bumpy texture.
// 
// Uniforms:
uniform mat4 view;
uniform mat4 proj;
uniform float animation_seconds;
uniform bool is_moon;
// Inputs:
//                     linearly interpolated from tessellation evaluation shader
//                     output
in vec3 sphere_fs_in;
in vec3 normal_fs_in;
in vec4 pos_fs_in; 
in vec4 view_pos_fs_in; 
// Outputs:
//               rgb color of this pixel
out vec3 color;
// expects: model, blinn_phong, bump_height, bump_position,
// improved_perlin_noise, tangent
void main()
{
  /////////////////////////////////////////////////////////////////////////////
  // light vector
  float theta = 0.5 * M_PI * animation_seconds; 
  vec4 light4 = view * vec4(3*cos(theta), 1.5, 3*sin(theta), 1);
  vec3 light3 = normalize(light4 - view_pos_fs_in).xyz;
  // view vector
  vec3 v = - normalize(view_pos_fs_in.xyz);


  // normal vector and bump height and bump position
  vec3 T; vec3 B;
  tangent(sphere_fs_in, T, B);
  float epsilon = 0.0001;
  vec3 bump = bump_position(is_moon, sphere_fs_in);
  vec3 dt = (bump_position(is_moon, sphere_fs_in + epsilon * T) - bump) / epsilon;
  vec3 db = (bump_position(is_moon, sphere_fs_in + epsilon * B) - bump) / epsilon;
  mat4 model = model(is_moon, animation_seconds);
  vec3 dn = normalize(cross(dt, db));
  vec3 n = normalize(transpose(inverse(view)) * transpose(inverse(model)) * vec4(dn, 1)).xyz;
  

  float phong = 5.0;
  vec3 ka = vec3(0.05, 0.05, 0.05);

  if (is_moon) {
    vec3 kd = vec3(0.5,  0.5,  0.5);
    vec3 ks = vec3(0.9,  0.9,  0.9);
    color = blinn_phong(ka, kd, ks, phong, n, v, light3);
  }
  else {
    vec3 kd = vec3(0.1,  0.1,  0.8);
    vec3 ks = vec3(0.9,  0.9,  0.9);
    color = blinn_phong(ka, kd, ks, phong, n, v, light3);
  }
  /////////////////////////////////////////////////////////////////////////////
}
