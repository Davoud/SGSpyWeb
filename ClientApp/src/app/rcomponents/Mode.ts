export interface Domain {
  name: string;
  components: RComponent[];
  opened: boolean;
}

export interface RComponent {
  name: string;
  version: string;
  id: string;
}

export interface REntity {
  name: string;
  id: string;
  opened: boolean;
  propertires: RProperty[]
}

export interface RProperty {
  name: string;
  type: string;
}
