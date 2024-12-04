// Add (hard code) an orbiting (point or directional) light to the scene. Light
// the scene using the Blinn-Phong Lighting Model.
//
// Uniforms:
uniform mat4 view;
uniform mat4 proj;
uniform float animation_seconds;
uniform bool is_moon;
// Inputs:
in vec3 sphere_fs_in;
in vec3 normal_fs_in;
in vec4 pos_fs_in; 
in vec4 view_pos_fs_in; 
// Outputs:
out vec3 color;
// expects: PI, blinn_phong
void main()
{
  /////////////////////////////////////////////////////////////////////////////
  // light vector
  float theta = 0.25 * M_PI * animation_seconds; 
  vec4 light4 = view * vec4(3*cos(theta), 3, 3*sin(theta), 1);
  vec3 light3 = normalize(light4 - view_pos_fs_in).xyz;
  // view vector
  vec3 v = - normalize(view_pos_fs_in.xyz);
  // normal vector 
  vec3 n = normalize(normal_fs_in);
  

  float phong = 800.0;
  vec3 ka = vec3(0.05, 0.05, 0.05);

  if (is_moon) {
    vec3 kd = vec3(0.5,  0.5,  0.5);
    vec3 ks = vec3(0.9,  0.9,  0.9);
    color = blinn_phong(ka, kd, ks, phong, n, v, light3);
  }
  else {
    vec3 kd = vec3(0.2,  0.2,  0.8);
    vec3 ks = vec3(0.9,  0.9,  0.9);
    color = blinn_phong(ka, kd, ks, phong, n, v, light3);
  }
  /////////////////////////////////////////////////////////////////////////////
}
