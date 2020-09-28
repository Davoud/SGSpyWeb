export interface Domain {
  name: string;
  components: RComponent[];
}

export interface RComponent {
  name: string;
  version: string;
  id: string;
}
